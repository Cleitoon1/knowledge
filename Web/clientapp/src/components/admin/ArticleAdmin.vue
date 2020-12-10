<template>
  <div class="article-admin">
    <b-form>
      <input id="article-id" type="hidden" v-model="article.id">
      <b-form-group label="Nome:" label-for="article-name">
        <b-form-input id="article-name" type="text" v-model="article.name" required 
          placeholder="Informe o Nome" :readonly="mode === 'remove'" />
      </b-form-group>
      <b-form-group label="Descrição:" label-for="article-description">
        <b-form-input id="article-description" type="text" v-model="article.description" required 
          placeholder="Informe a Descrição" :readonly="mode === 'remove'" />
      </b-form-group>
      <b-form-group v-if="mode === 'save'" label="Imagem (URL):" label-for="article-imageUrl">
        <b-form-input id="article-imageUrl" type="text" v-model="article.imageUrl" required 
          placeholder="Informe a Url da Imagem" :readonly="mode === 'remove'" />
      </b-form-group>
      <b-form-group  v-if="mode === 'save'" label="Categoria:" label-for="article-categoryId">
        <b-form-select id="article-categoryId" :options="categories" v-model="article.categoryId" />
      </b-form-group>
      <b-form-group  v-if="mode === 'save'" label="Autor:" label-for="article-userId">
        <b-form-select id="article-userId" :options="users" v-model="article.userId" />
      </b-form-group>
      <b-form-group v-if="mode === 'save'" label="Conteudo:" label-for="article-content">
        <VueEditor v-model="article.content" placeholder="Informe o conteúdo do artigo" />
      </b-form-group>
      <b-button variant="primary" v-if="mode === 'save'" @click="save">Salvar</b-button>
      <b-button variant="danger" v-if="mode === 'remove'" @click="remove">Excluir</b-button>
      <b-button class="ml-2" @click="reset">Cancelar</b-button>      
    </b-form>
    <hr>
      <b-table hover striped :items="articles" :fields="fields">
        <template slot="actions" slot-scope="data">
          <b-button variant="warning" @click="loadarticle(data.item)" class="mr-2">
            <i class="fa fa-pencil"></i>
          </b-button>
          <b-button variant="danger" @click="loadarticle(data.item, 'remove')" class="mr-2">
            <i class="fa fa-trash"></i>
          </b-button>
        </template>
      </b-table>
      <b-pagination size="md" v-model="page" :total-rows="count" :per-page="limit" />
  </div>
</template>

<script>
import { VueEditor } from 'vue2-editor'
import { showError } from '@/global'

export default {  
  name: 'ArticleAdmin',
  components: {
    VueEditor
  },
  data: function () {
      return {
        mode: 'save',
        article: {

        },
        categories: [],
        users: [],
        page: 1,
        limit: 0,
        count: 0,
        articles: [],
        fields: [
          {key: 'id', label: 'Código', sortable: true},
          {key: 'name', label: 'Nome', sortable: true},
          {key: 'description', label: 'Descrição', sortable: true},
          {key: 'actions', label: 'Ações'}
        ]
      }
  },
  methods: {
    loadArticles() {
      this.$http.get(`/articles/all?page=${this.page}`).then(res => {
         this.articles = res.data.data
         this.data = res.data.count
         this.limit = res.data.limit
      })
    },
    reset() {
      this.mode = 'save'
      this.article = {}
      this.loadArticles();
    },
    save() {
      this.$http.post(`/articles/${this.article.id ? `update` : `create`}`, this.article)
        .then(() => {
          this.$toasted.global.defaultSuccess()
          this.reset()
        }).catch(showError)
    },
    remove() {
       const id = this.article.id ? `/${this.article.id}` : ''
        this.$http.delete(`/articles/delete/${id}`)
        .then(() => {
          this.$toasted.global.defaultSuccess()
          this.reset()
        }).catch(showError)
    },
    loadArticle(article, mode = 'save'){
      this.mode = mode
      this.$http.get(`/articles/get/${article.id}`)
        .then(res => this.article = res.data.data)
    },
    loadCategories() {
      this.$http.get(`/categories/all`).then(res => {
        this.categories = res.data.data.map(category => {
          return { value: category.id, text: category.path }
        })
      })
    },
    loadUsers() {
      this.$http.get(`/users/all`).then(res => {
        this.users = res.data.map(user => {
          return { value: user.id, text: user.name }
        })
      })
    }
  },
  watch: {
    page() {
      this.loadArticles()
    }
  },
  mounted(){
    this.loadCategories();
    this.loadUsers();
    this.loadArticles();    
  }
}
</script>

<style>

</style>