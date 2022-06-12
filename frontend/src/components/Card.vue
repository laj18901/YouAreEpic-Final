<template>
  <button class="card" :style="filterStyle" @click="show = !show">
    <span class="title">{{title}}</span>
    <img :src="source" alt="Tier" class="categoryImage">
  </button>
</template>

<script>

export default {
  name: 'Card',
  props: {
    source: String,
    title: String
  },
  data () {
    return {
      show: false
    }
  },
  computed: {
    filterStyle () {
      if (this.show) {
        return {
          border: 'none',
          borderRadius: '2pt',
          boxShadow: '0 0 0 2pt #3A93A7',
          outline: 'none',
          transition: '.1s',
          backgroundColor: '#E8F1F3'
        }
      } else {
        return ''
      }
    }
  }

}
</script>

<style>


.categoryImage{
  height: auto;
  width: clamp(4rem,15vw,12rem);
}



.title{
  font-size: clamp(.5rem,5vw,3rem);
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
}

</style>
