import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex);
export default new Vuex.Store({
    state: {
        isMenuVisible: true,
        user: {
            name: 'Usuario Mock',
            email: 'mock@teste.com.br'
        }
    },
    mutations: {
        toggleMenu(state, isVisible) {
            state.isMenuVisible = isVisible === undefined ? !state.isMenuVisible : isVisible
        }
    }
})