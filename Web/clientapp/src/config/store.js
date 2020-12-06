import axios from 'axios'
import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex);
export default new Vuex.Store({
    state: {
        isMenuVisible: false,
        user: null
    },
    mutations: {
        toggleMenu(state, isVisible) {
            if(!state.user){
                state.isMenuVisible = false
                return
            }
            state.isMenuVisible = isVisible === undefined ? !state.isMenuVisible : isVisible
        },
        setUser(state, user) {
            state.user = user
            if(user) {
                state.isMenuVisible = true
                axios.defaults.headers.common['Authorization'] = `bearer ${user.token}`
            } else {
                state.isMenuVisible = false
                delete axios.defaults.headers.common['Authorization']
            }
        }
    }
})