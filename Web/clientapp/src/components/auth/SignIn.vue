<template>
    <div class="signin">
        <div class="auth-title">Login</div>
        <form-group :validator="$v.mail" attribute="E-mail">
            <input v-model="mail" name="mail" type="text" placeholder="E-mail">
        </form-group>
        <form-group :validator="$v.mail" attribute="Senha">        
            <input v-model="password" name="password" type="password" placeholder="Senha">
        </form-group>
        <button @click="signin">Entrar</button>
    </div>
</template>

<script>
import { required } from "vuelidate/lib/validators";

export default {
    name: 'SignIn',
    props: {
        sendData: {
            Type: Function,
            required: true
        }
    },
    data: function () {
        return {
            mail: '',
            password: ''
        }
    },
    validations: {
        mail: { required },
        password: { required }
        
    },
    methods: {
        signin() {
            this.$v.$touch()
            if (!this.$v.$invalid) { 
                this.sendData({mail: this.mail, password: this.password })
            }
        },
        reset() {
            this.mail = '';
            this.password = ''
        }
    }
}
</script>
<style scoped>
    .signin {
        width: 100%;
    }    
</style>