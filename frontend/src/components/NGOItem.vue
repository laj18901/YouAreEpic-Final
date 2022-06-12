<template>
  <div  class="wrapper" :style="selectedStyleWrapper" >
  <button class="item" :style="selectedStyleButton" @click="openDetails">
    <img :src=logo alt="" class="ngologo">
    <span class="ngoname">{{name}} </span>

  </button>
  <div v-if="openDetail" class="details" >
  <p class="detail">{{shortDescription}}</p>
    <!--<div id="buttons">-->
    <div class="buttons">
    <button class="button" @click="openWebsite">Website</button>
    <button class="button" @click="openPayment">Spenden</button>
    </div>
    <!--</div>-->
  </div>
  </div>
</template>

<script>
export default {
  name: 'ngoitem',
  props: {
    logo: String,
    name: String,
    description: String,
    shortDescription: String,
    website: String,
    id: String
  },

  data () {
    return {
      openDetail: false
    }
  },

  methods: {
    openDetails () {
      this.openDetail = !this.openDetail
    },

    openPayment () {
      this.$router.push(`/Payment/${this.id}`)
    },

    openWebsite () {
      window.open(this.website)
    }

  },
  computed: {
    selectedStyleButton () {
      if (this.openDetail) {
        return {
          backgroundColor: '#BA2660'
        }
      } else {
        return ''
      }
    },
    selectedStyleWrapper () {
      if (this.openDetail) {
        return {
          border: 'none',
          boxShadow: '0 0 0 2pt #BA2660',
          outline: 'none',
          transition: '.1s'
        }
      } else {
        return ''
      }
    }
  }
}
</script>

<style scoped>

</style>
