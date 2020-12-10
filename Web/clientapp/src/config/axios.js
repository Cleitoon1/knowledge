import axios from 'axios'
import Vue from 'vue'
import { baseApiUrl } from '@/global'

const sucess = res => res
const error = err => {
    if(err.response.status == 401) {
        window.location = '/'
    } else {
        return Promise.reject(err)
    }
}

axios.interceptors.response.use(sucess, error)
axios.defaults.baseURL = baseApiUrl;
Vue.prototype.$http = axios;