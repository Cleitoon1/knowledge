import 'font-awesome/css/font-awesome.css'
import Vue from 'vue'
import App from './App.vue'
import './config/bootstrap'
import store from './config/store'
import router from './config/router'

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app')
