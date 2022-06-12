<template>
  <div class="categories" >

      <card v-for="c in categories"  :key='c.id'  @click="selectedCategories.push(c.id)" :title="c.name" :source="c.imageLink"></card>


  </div>
  <div class="buttons">
    <button class="button" style="width: 40%" @click.native="filteredSelect">Auswahl bestätigen</button>
    <br>
    <button class="button"  style="width: 40%" @click.native="allSelect" >Alles Anzeigen </button>
  </div>
</template>

<script>
import Card from '../components/Card.vue'
import axios from 'axios'

export default {
  name: 'App',
  components: {
    Card
  },
  data () {
    return {
      categories: [],
      selectedCategories:[],
      user: ''
    }
  },
  methods: {
    filteredSelect () {
      let categories = this.selectedCategories
      this.$router.push({ name: 'NGOList', query: { categories } })
    },

    allSelect() {
      let categories = []
      this.$router.push({ name: 'NGOList', query: { categories } })
    }
  },
  mounted(){
    axios
        .get('api/categories')
        .then(response => {
            this.categories = response.data.categories
          }).catch(e => console.log(e));
  }
}
</script>

<style scoped>
  .categories {
    display: grid;
    grid-template-columns: 1fr 1fr;
    grid-auto-flow: row;
    grid-template-rows: 1fr 1fr;
    gap: clamp(0.25rem,1vw,1rem);
  }

  .buttons{
    margin-top: 1.5rem;
    display: flex;
    justify-content: center;
  }

</style>
