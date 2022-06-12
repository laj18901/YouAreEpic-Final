<template>
  <div style="width: 100%; height: 50%; max-height: 10rem ; border-bottom: 1px solid black; text-align: center">
  <span style="" class="seperator">
    {{text}}
  </span>
  </div>
</template>

<script>
export default {
  name: 'Seperator',
  props: {
    text: String
  }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css?family=Jost');
.seperator{
  background-color: #FFFFFF;

  font-size: clamp(1.5rem,5vw,3rem);
  font-family: Jost;
  font-style: normal;
  font-weight: bold;
  text-transform: uppercase;
}

</style>
