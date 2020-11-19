import Vue from 'vue'
import Toasted from 'vue-toasted'

Vue.use(Toasted, {
    iconPage: 'fontawesome',
    duration: 3000
})

Vue.toasted.register(
    'defaultSuccess',
    'Operação Realizada com sucesso !',
    { type: 'success', icon: 'check' }
)

Vue.toasted.register(
    'defaultError',
    payload => !payload.msg ? 'Oops.. Erro inesperado' : payload.msg,
    { type: 'error', icon: 'times' }
)