<template>
  <div class="user-admin">
    <b-form>
      <input id="user-id" type="hidden" v-model="user.id">
      <b-row>
        <b-col md="6" sm="12">
          <b-form-group label="Nome:" label-for="user-name">
            <b-form-input id="user-name" type="text" v-model="user.name" required 
              placeholder="Informe o Nome" :readonly="mode === 'remove'" />
          </b-form-group>
        </b-col>
        <b-col md="6" sm="12">
          <b-form-group label="Ultimo Nome:" label-for="user-lastName">
            <b-form-input id="user-lastName" type="text" v-model="user.lastName" required 
              placeholder="Informe o Ultimo Nome" :readonly="mode === 'remove'" />
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col md="6" sm="12">
          <b-form-group label="E-mail:" label-for="user-mail">
            <b-form-input id="user-mail" type="text" v-model="user.mail" required 
              placeholder="Informe o E-mail" :readonly="mode === 'remove'" />
          </b-form-group>
        </b-col>
        <b-col md="6" sm="12">
          <b-form-group label="Perfil:" label-for="user-profileId">
            <b-form-select v-if="mode === 'save'" v-model="user.profileId" :options="profiles" />
            <b-form-input v-else id="user-profileId" type="text" v-model="user.profileName" readonly />
          </b-form-group>
        </b-col>
      </b-row>
    
      <b-row v-show="mode === 'save'">
        <b-col md="6" sm="12">
          <b-form-group label="Senha:" label-for="user-password">
            <b-form-input id="user-password" type="password" v-model="user.password" required 
              placeholder="Informe a Senha" :readonly="mode === 'remove'" />
          </b-form-group>
        </b-col>
        <b-col md="6" sm="12">
          <b-form-group label="Confirmação de senha:" label-for="user-confirm-password">
            <b-form-input id="user-confirm-password" type="password" v-model="user.confirmPassword" required 
              placeholder="Confirme a Seha" :readonly="mode === 'remove'" />
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col xs="12">
          <b-button variant="primary" v-if="mode === 'save'" @click="save">Salvar</b-button>
          <b-button variant="danger" v-if="mode === 'remove'" @click="remove">Excluir</b-button>
          <b-button class="ml-2" @click="reset">Cancelar</b-button>
        </b-col>
      </b-row>      
    </b-form>
    <hr>
      <b-table hover striped :items="users" :fields="fields">
        <template slot="actions" slot-scope="data">
          <b-button variant="warning" @click="loadUser(data.item)" class="mr-2">
            <i class="fa fa-pencil"></i>
          </b-button>
          <b-button variant="danger" @click="loadUser(data.item, 'remove')" class="mr-2">
            <i class="fa fa-trash"></i>
          </b-button>
        </template>
      </b-table>
  </div>
</template>

<script>
import { showError } from '@/global'

export default {  
  name: 'UserAdmin',
  data: function () {
      return {
        mode: 'save',
        user: {

        },
        users: [],
        profiles: [],
        fields: [
          {key: 'id', label: 'Código', sortable: true},
          {key: 'name', label: 'Nome', sortable: true},
          {key: 'mail', label: 'E-mail', sortable: true},
          {key: 'profileName', label: 'Perfil', sortable: true },
          {key: 'actions', label: 'Ações'}
        ]
      }
  },
  methods: {
    async loadProfiles() {
      await this.$http.get(`/profiles/all`).then(res => {
          this.profiles = res.data.map(profile => {
            return { ...profile, value: profile.id, text: profile.name }
          })
      })
    },
    loadUsers() {
      this.$http.get(`/users/all`).then(res => {
        this.users = res.data.map(user => {
          const profile = this.profiles.find(e => e.id === user.profileId);
          return  { ...user, password: '', profileName: (profile ? profile.name : '') }
        })
      })
    },
    reset() {
      this.mode = 'save'
      this.user = {}
      this.loadUsers();
    },
    save() {
      this.$http.post(`/users/${this.user.id ? `update` : `create`}`, this.user)
        .then(() => {
          this.$toasted.global.defaultSuccess()
          this.reset()
        }).catch(showError)
    },
    remove() {
       const id = this.user.id ? `${this.user.id}` : ''
        this.$http.delete(`/users/delete/${id}`, this.user)
        .then(() => {
          this.$toasted.global.defaultSuccess()
          this.reset()
        }).catch(showError)
    },
    loadUser(user, mode = 'save'){
      this.mode = mode
      this.user = { ...user }
    }
  },
  mounted(){
    this.loadProfiles();
    this.loadUsers();    
  }
}
</script>

<style>

</style>