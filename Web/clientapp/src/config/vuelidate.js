import Vue from 'vue'
import Vuelidate from 'vuelidate'
import VuelidateErrorExtractor, { templates } from 'vuelidate-error-extractor'

import FormGroup from "../components/templates/FormGroup.vue";
import FormSummary from "../components/templates/FormSummary.vue";

Vue.use(Vuelidate);
Vue.use(VuelidateErrorExtractor, {
    i18n: false,
    messages: {
        required: `Este campo é obrigatório`,
        requiredIf:`Obrigatório se o campo '{field}' estiver preenchido`,
        requiredUnless: `Obrigatório se o campo '{field}' não estiver preenchido`,
        minLength:`Tamanho mínimo de {min} caracteres é obrigatório`,
        maxLength: `Tamanho máximo de {max} foi excedido`,
        minValue: `Valor mínimo de {min} é obrigatório`,
        maxValue: `Valor máximo de {max} foi excedido`,
        between: `Tamanho do campo precisa ser entre {params.min} e {params.max}`,
        alpha: `O campo só pode conter letras`,
        alphaNum: `O campo só pode conter letras e números`,
        numeric: `O campo só pode conter números`,
        integer: `O campo deve ser um número inteiro`,
        decimal: `O campo deve conter um valor decimal`,
        email: `Informe um endereço de e-mail válido`,
        ipAddress: `Informe um endereço IP válido`,
        macAddress: `Informe um endereço MAC válido`,
        sameAs: `Valor informado deve ser igual ao campo {same}`,
        sameAsPassword: `Valor Informado deve ser igual ao campo de Senha`,
        url: `Informe uma URL válida`
    }
  })

  Vue.component("FormGroup", FormGroup);
  Vue.component("FormSummary", FormSummary);
  Vue.component("formWrapper", templates.FormWrapper);
