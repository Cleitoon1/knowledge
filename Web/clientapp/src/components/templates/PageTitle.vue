<template>
  <div class="page-title">
    <h1><i v-if="icon" :class="icon"></i> {{main}}</h1>
    <h2>{{ sub }}</h2>
    <h4 v-if="obs">{{obs}}</h4>
    <hr />
  </div>
</template>

<script>
export default {
    name: 'PageTile',
    props: ['icon', 'main', 'sub', 'obs']
}
</script>

<style>
    .page-titile h1 {
        margin: 0px;
    }

    .page-title h2 {
        color: #777;
        margin-top: 5px;
        font-size: 1.3rem;
    }
    
    .page-title h4 {
        color: #777;
        margin-top: 5px;
        font-size: 1rem;
    }
</style>