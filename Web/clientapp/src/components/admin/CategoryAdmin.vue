<template>
  <div class="category-admin">
    <b-form>
      <input id="category-id" type="hidden" v-model="category.id">
      <b-form-group label="Nome:" label-for="category-name">
        <b-form-input id="category-name" type="text" v-model="category.name" required 
          placeholder="Informe o Nome" :readonly="mode === 'remove'" />
      </b-form-group>
      <b-form-group label="Categoria Pai:" label-for="category-parentCategoryId">
        <b-form-select v-if="mode === 'save'" v-model="category.parentCategoryId" :options="categories" />
        <b-form-input v-else id="category-parentCategoryId" type="text" v-model="category.path" readonly />
      </b-form-group>
      <b-button variant="primary" v-if="mode === 'save'" @click="save">Salvar</b-button>
      <b-button variant="danger" v-if="mode === 'remove'" @click="remove">Excluir</b-button>
      <b-button class="ml-2" @click="reset">Cancelar</b-button>      
    </b-form>
    <hr>
      <b-table hover striped :items="categories" :fields="fields">
        <template slot="actions" slot-scope="data">
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
import axios from 'axios'
import { baseApiUrl, showError } from '@/global'

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
  methods: {
    loadCategories() {
      const url = `${baseApiUrl}/api/categories/all`
      axios.get(url).then(res => {
        this.categories = res.data.data.map(category => {
          return { ...category, value: category.id, text: category.path }
        })
      })
    },
    reset() {
      this.mode = 'save'
      this.category = {}
      this.loadCategories();
    },
    save() {
      const url = `${baseApiUrl}/api/${this.category.id ? `categories/update` : `categories/create`}`
      axios.post(url, this.category)
        .then(() => {
          this.$toasted.global.defaultSuccess()
          this.reset()
        }).catch(showError)
    },
    remove() {
       const id = this.category.id ? `${this.category.id}` : ''
        axios.delete(`${baseApiUrl}/api/categories/delete/${id}`)
        .then(() => {
          this.$toasted.global.defaultSuccess()
          this.reset()
        }).catch(showError)
    },
    loadCategory(category, mode = 'save'){
      this.mode = mode
      this.category = { id: category.id, name: category.name, parentCategoryId : category.parentCategoryId }
    }
  },
  mounted(){
    this.loadCategories();    
  }
}
</script>

<style>
</style>