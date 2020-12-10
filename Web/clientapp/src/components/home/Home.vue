<template>
    <div class="home">
        <PageTitle icon="fa fa-home" main="DashBoard" sub="Base de Conhecimento" />
        <div class="stats">
            <Stat title="Categorias" :value="stat.categories" icon="fa fa-folder" color="#d54d50" />
            <Stat title="Artigos" :value="stat.articles" icon="fa fa-file" color="#3bc480" />
            <Stat title="Usuários" :value="stat.users" icon="fa fa-user" color="#3282cd" />
        </div>
    </div>
</template>

<script>
import PageTitle from '../templates/PageTitle'
import Stat from './Stat'

export default {
    name: 'Home',
    components: {PageTitle, Stat},
    data: function () {
        return {
            stat: {}
        }
    },
    methods: {
        getStats() {
            this.$http.get(`/stats/getstats`).then(res => this.stat = res.data)
        }
    },
    mounted() {
        this.getStats()
    }
}
</script>

<style>
    .stats {
        display: flex;
        justify-content: space-between;
        flex-wrap: wrap;
    }
</style>