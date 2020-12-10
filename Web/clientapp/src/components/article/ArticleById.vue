<template>
    <div class="article-by-id">
        <page-title icon="fa fa-file-o" :main="article.name" :sub="article.description" />
        <div class="article-content" v-html="article.content"></div>
    </div>
</template>

<script>
import 'highlightjs/styles/dracula.css'
import hlsj from 'highlightjs/highlight.pack'
import PageTitle from '../templates/PageTitle'
export default {
    name: 'ArticleById',
    components: { PageTitle },
    data: function () {
        return {
            article: {}
        }
    },
    mounted() {
        this.$http(`/articles/all?categoryId=${this.$route.params.id ? this.$route.params.id : ''}`)
            .then(res => this.article = res.data.data)
    },
    updated() {
        document.querySelectorAll('.article-content pre.ql-syntax').forEach(e => {
            hlsj.highlightBlock(e)
        })
    }
}
</script>

<style>
    .article-content {
        background-color: #fff;
        border-radius: 8px;
        padding: 25px;
    }

    .article-content-pre {
        padding: 20px;
        border-radius: 8px;
        font-size: 1.2rem;
    }

    .article.content img {
        max-width: 100%;
    }

    .article-content :last-child {
        margin-bottom: 0px;
    }
</style>