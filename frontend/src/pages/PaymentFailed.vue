<template>
  <div class="wrapper">
    <h1 class="header">SORRY!</h1>
    <p class="text">Die Bezahlung konnte leider nicht erfolgreich durchgeführt werden</p>
    <button class="button" style="align-self: center" @click="this.$router.go(-1)">Nochmal versuchen</button>
  </div>

</template>

<script>
export default {
name: "PaymentFailed"
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css?family=Jost');

.wrapper{
  text-align: center;
  font-family: Jost;
  font-style: normal;
  margin-top: 10rem;
  color: #BA2660;
}

.header{
  font-size: 3rem;
}

.text{
  font-size: 1.5rem;
}

</style>