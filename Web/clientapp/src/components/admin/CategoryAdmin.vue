<template>
  <div class="category-admin">
    <b-form>
      <input id="category-id" type="hidden" v-model="category.id">
      <form-group :validator="$v.category.name" attribute="Nome" label="Nome:" label-for="category-name">
        <b-form-input id="category-name" type="text" v-model="category.name" required 
          placeholder="Informe o Nome" :readonly="mode === 'remove'" />
      </form-group>
      <form-group :validator="$v.category.parentId" attribute="Categoria Pai" label="Categoria Pai:"
        label-for="category-parentId">
        <b-form-select v-if="mode === 'save'" v-model="category.parentId" :options="categories" />
        <b-form-input v-else id="category-parentId" type="text" v-model="category.path" readonly />
      </form-group>
      <b-button variant="primary" v-if="mode === 'save'" @click="save">Salvar</b-button>
      <b-button variant="danger" v-if="mode === 'remove'" @click="remove">Excluir</b-button>
      <b-button class="ml-2" @click="reset">Cancelar</b-button>      
    </b-form>
    <hr>
      <b-table hover striped :items="categories" :fields="fields">
        <template slot="cell(actions)" slot-scope="data">
          <b-button variant="warning" @click="loadCategory(data.item)" class="mr-2">
            <i class="fa fa-pencil"></i>
          </b-button>
          <b-button variant="danger" @click="loadCategory(data.item, 'remove')" class="mr-2">
            <i class="fa fa-trash"></i>
          </b-button>
        </template>
      </b-table>
  </div>
</template>

<script>
import { showError } from '@/global'
import { required } from "vuelidate/lib/validators";

export default {  
  name: 'CategoryAdmin',
  data: function () {
      return {
        mode: 'save',
        category: {
        },
        categories: [],
        fields: [
          {key: 'id', label: 'Código', sortable: true},
          {key: 'name', label: 'Nome', sortable: true},
          {key: 'path', label: 'Caminho', sortable: true},
          {key: 'actions', label: 'Ações'}
        ]
      }
  },
  validations: {
    category: {
        name: { required },

    }
  },
  methods: {
    loadCategories() {
      this.$http.get(`/categories/all`).then(res => {
        this.categories = res.data.data.map(category => {
          return { ...category, value: category.id, text: category.path }
        })
      })
    },
    reset() {
      this.mode = 'save'
      this.category = {}
      this.$v.$reset();
      this.loadCategories();
    },
    save() {
      this.$v.$touch()
      if (!this.$v.$invalid) {      
        this.$http.post(`/categories/${this.category.id ? `update` : `create`}`, this.category)
          .then(() => {
            this.$toasted.global.defaultSuccess()
            this.reset()
          }).catch(showError)
      }
    },
    remove() {
       const id = this.category.id ? `${this.category.id}` : ''
        this.$http.delete(`/categories/delete/${id}`)
        .then(() => {
          this.$toasted.global.defaultSuccess()
          this.reset()
        }).catch(showError)
    },
    loadCategory(category, mode = 'save'){
      this.mode = mode
      this.category = { id: category.id, name: category.name, parentId : category.parentId }
    }
  },
  mounted(){
    this.loadCategories();    
  }
}
</script>

<style>
</style>