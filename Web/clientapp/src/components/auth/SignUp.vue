<template>
    <div class="signup">
        <div class="auth-title">Cadastro</div>

        <form-group :validator="$v.user.name" attribute="E-mail">
            <input v-model="user.name" type="text" placeholder="Nome">
        </form-group>

        <form-group :validator="$v.user.lastName" attribute="Ultimo Nome"> 
            <input v-model="user.lastName" type="text" placeholder="Ultimo Nome">
        </form-group>

        <form-group :validator="$v.user.mail" attribute="E-mail"> 
            <input v-model="user.mail" name="mail" type="text" placeholder="E-mail">
        </form-group>

        <form-group :validator="$v.user.mobilePhone" attribute="Celular"> 
            <input v-model="user.mobilePhone" type="text" placeholder="Celular">
        </form-group>

        <form-group :validator="$v.user.password" attribute="Senha"> 
            <input v-model="user.password" name="password" type="password" placeholder="Senha">
        </form-group>

        <form-group :validator="$v.user.confirmPassword" attribute="Confirmação de Senha"> 
            <input v-model="user.confirmPassword" type="password" placeholder="Confirme a Senha">
        </form-group>

        <button @click="signup">Cadastrar</button>
    </div>
</template>

<script>
import { required, minLength, email, numeric, sameAs } from "vuelidate/lib/validators";

export default {
    name: 'SignUp',
    props: {
        sendData: {
            Type: Function,
            required: true
        }
    },
    data: function () {
        return {
            user: { }
        }
    },

    methods: {
        signup() {
            this.$v.$touch()
            if (!this.$v.$invalid) { 
                this.sendData(this.user)
            }
        },
        reset() {
            this.user = { }
        }
    },
    validations: {
        user: {
            name: { required },
            lastName: { required },
            mail: { required, email },
            mobilePhone: { numeric },
            password: { required, minLength: minLength(6) },
            confirmPassword: { required, sameAsPassword : sameAs('password') }
        }
    },
}
</script>
<style scoped>
    .signup {
        width: 100%;
    }
</style>