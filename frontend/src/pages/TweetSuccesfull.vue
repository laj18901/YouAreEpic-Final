<template>
  <div class="wrapper">
    <h1 class="header">EPISCH!</h1>
    <p class="text">Dein Tweet wurde gepostet</p>
    <button @click="openTwitter" class="button" style="align-self: center; word-wrap: break-word; width: 50%">Schau dir deinen Tweet an
    </button>
  </div>


</template>

<script>
import Tweet from 'vue-tweet'
import axios from 'axios'

export default {
    name: "TweetSuccessful",
    components: {
      Tweet
    },
  data () {
    return {
      tweetlink: null
    }
  },
  methods:{
    openTwitter(){
      window.open(this.tweetlink);
    }
  },
    mounted(){
        let x = this.$route.query;
        let id = x["id"];
        let name = x["name"];
       
        this.tweetlink = `https://twitter.com/${name}/status/${id}`
    },


}
</script>

<style scoped lang="scss">
@import url('https://fonts.googleapis.com/css?family=Jost');


.wrapper{
  text-align: center;
  font-family: Jost;
  font-style: normal;
  margin-top: 10rem;
  color: #3A93A7;
  display: flex;
  flex-direction: column;
}

.header{
  font-size: 3rem;
}

.text{
  font-size: 1.5rem;
}

</style>