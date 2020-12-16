<template>
    <div class="auth-content">
        <div class="auth-modal">
            <img src="@/assets/logo.png" width="200" alt="Logo" />
            <hr>
            <sign-up v-if="showSignup" :sendData="signup" />
            <sign-in v-else :sendData="signin" />
            <a href @click.prevent="showSignup = !showSignup">
                <span v-if="showSignup">Já tem cadastro? Acesse o Login!</span>
                <span v-else>Não tem cadastro? Registre-se aqui!</span>
            </a>
        </div>
    </div>
</template>

<script>
import { showError, userKey } from '@/global'
import SignIn from './SignIn'
import SignUp from './SignUp'
export default {
    name: 'Auth',
    components: { SignIn, SignUp },
    data: function() {
        return {
            showSignup: false        
        }
    },
    methods: {
        signin(data) {
            this.$http.post(`/auth/signin`, { mail: data.mail, password: data.password })
            .then(res => {
                this.$store.commit('setUser', res.data)
                localStorage.setItem(userKey, JSON.stringify(res.data))
                this.$router.push({ path: '/' })
            }).catch(showError)
        },
        signup(data) {
            this.$http.post(`/auth/signup`, data)
            .then(() => {
                this.$toasted.global.defaultSuccess()
                this.showSignup = false
            }).catch(showError)
        },
    }
}
</script>

<style>
    .auth-content {
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .auth-modal {
        background-color: #FFF;
        width: 350px;
        padding: 35px;
        box-shadow: 0 1px 5px rgba(0, 0, 0, 0.15);

        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .auth-title {
        font-size: 1.2rem;
        font-weight: 100;
        margin-top: 10px;
        margin-bottom: 15px;
    }

    .auth-modal .form-group {
        width: 100%;
        margin: 0px 0px 15px 0px;
        padding: 0;
    }

    .auth-modal .form-summary {
        width: 100%;
        margin: 0px 0px 15px 0px;
        padding: 0;
    }

    .auth-modal input {
        border: 1px solid #BBB;
        width: 100%;
        padding: 3px 8px;
        outline: none;
    }

    .auth-modal button {
        align-self: flex-end;
        background-color: #2460ae;
        color: #FFF;
        padding: 5px 15px;
    }

    .auth-modal a {
        margin-top: 35px;
    }

    .auth-modal hr {
        border: 0;
        width: 100%;
        height: 1px;
        background-image: linear-gradient(to right,
            rgba(120, 120, 120, 0),
            rgba(120, 120, 120, 0.75),
            rgba(120, 120, 120, 0));
    }
</style>
