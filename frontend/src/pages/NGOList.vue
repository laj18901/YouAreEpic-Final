<template>
  <div>
    <div id="items" v-for="ngo in ngos" >
      <ngoitem :logo=ngo.logoLink :name=ngo.name :description=ngo.description :short-description="ngo.shortDescription" :website=ngo.websiteLink :id=ngo.id></ngoitem>
    </div>
  </div>

</template>

<script>
import ngoitem from '../components/NGOItem.vue'
import axios from 'axios'

export default {
  name: 'NGOList',
  components: {
    ngoitem
  },
  data () {
    return {
      ngos: {}
    }
  },

  async mounted () {
   
  await fetch("/api/npos-by-category", {
        headers: [
            [ "Content-Type", "application/json" ]
        ],
        method:"POST",
        body:JSON.stringify({
          categories:this.$route.query.categories
        })

      })
      
      .then(response => response.json()).then(response => this.ngos = response.npOs)
      .catch(function (error) {
    });
  }

}
</script>

<style scoped>
#app{
  color: #2c3e50;
 /* margin-top: 10px;*/
  justify-content: center;

}

#items{
  align-content: center;
  margin-top: 20px;
}

</style>
