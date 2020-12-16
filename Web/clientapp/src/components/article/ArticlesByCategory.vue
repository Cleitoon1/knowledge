<template>
  <div class="arcitles-by-category">
      <page-title icon="fa fa-folder-o" :main="category.name" sub="Categoria" />
      <ul>
          <li v-for="article in articles" :key="article.id">
              <article-item :article="article" />
          </li>
      </ul>
      <div class="load-more">
          <button v-if="loadMore" class="btn btn-lg btn-outline-primary" @click="getArticles">
              Carregar mais artigos
          </button>
      </div>
  </div>
</template>

<script>
import PageTitle from '../templates/PageTitle'
import ArticleItem from './ArticleItem'

export default {
    name: 'ArticlesByCategory',
    components: { PageTitle, ArticleItem },
    data: function () {
        return {
            category: {},
            articles: [],
            page: 1,
            loadMore: true
        }
    },
    methods: {
        getCategory() {
            this.$http(`/categories/get/${this.category.id}`)
                .then(res => this.category = res.data)
        },
        getArticles() {
            this.$http(`/articles/all?categoryId=${this.category.id}&page=${this.page}&quantity=3`)
                .then(res => {
                    this.articles = this.articles.concat(res.data.data)
                    this.page++
                    if(res.data.lenght === 0) this.loadMore = false
            })
        }
    },
    watch: {
        $route(to) {
            this.category.id = to.params.id
            this.articles = []
            this.page = 1;
            this.loadMore = true
            this.getCategory();
            this.getArticles();
        }
    },
    mounted() {
        this.category.id = this.$route.params.id
        this.getCategory();
        this.getArticles();
    }
}
</script>

<style>
    .articles-by-category ul {
        list-style-type: none;
        padding: 0px;
    }

    .articles-by-category .load-more {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-top: 25px;
    }
</style>