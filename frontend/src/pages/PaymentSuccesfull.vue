<template>
  <div class="wrapper">
    <h1 class="header">DANKE!</h1>
    <p class="text">Die Spende wurde erfolgreich durchgeführt!</p>
    <button @click="openPost" class="button" style="align-self: center; word-wrap: break-word; width: 50%;">Teile jetzt deinen Epic Moment mit deiner Community
    </button>
  </div>

</template>

<script>
export default {
name: "PaymentSuccesfull",
data(){
  return{
   ngoid : null,
   ammount: 0
  };
 
},
methods:{
  openPost(){
    this.$router.push(`/post?ngo=${this.ngoid}&ammount=${this.ammount}`);
  }
},
 mounted(){
        let session = this.$route.query["session_id"];
        fetch(`/api/payment/checkout-session?sessionid=${session}`)
        .then(r => r.json())
        .then(a => {
          console.log(a)
          this.ngoid = a.ngoid
          this.ammount = parseFloat(a.ammount)/100
        })
        .catch(e => console.log(e));
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