<template>
   <b-form-invalid-feedback
     :force-show="hasErrors"
   >
    <div v-for="(error, index) in activeErrorMessages" :key="index">
      <div>{{ error }}</div>
    </div>
  </b-form-invalid-feedback>  
</template>
<script>
import { multiErrorExtractorMixin } from "vuelidate-error-extractor";
export default {
  name: "FormSummary",
  extends: multiErrorExtractorMixin
};
</script>