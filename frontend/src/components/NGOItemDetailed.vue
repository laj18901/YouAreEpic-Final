<template>
  <div class="wrapperDetailed" >
  <div class="itemDetailed" >
    <img :src=logo alt="" class="ngologo">
    <span class="ngoname">{{name}}
    </span>
  </div>
  <div class="detailedDetails">
  <p class="detailedDetail">{{shortDescription}}</p>
  </div>
  </div>
</template>

<script>
export default {
  name: 'ngoitemdetailed',
  props: {
    logo: String,
    name: String,
    description: String,
    shortDescription: String,
    website: String,
    id: String
  }
}

</script>

<style scoped>


</style>
