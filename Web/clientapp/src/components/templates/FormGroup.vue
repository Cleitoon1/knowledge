<template>
  <b-form-group
    :invalid-feedback="firstErrorMessage"
    :state="isValid"
    :label="label"
  >
    <slot
      :attrs="{ state: isValid }"
      :listeners="{ input: () => preferredValidator.$touch() }"
    />
  </b-form-group>
</template>
<script>
import { singleErrorExtractorMixin } from "vuelidate-error-extractor";

export default {
  name: "FormElement",
  extends: singleErrorExtractorMixin
};
</script>
