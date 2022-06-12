<template>


  <div class="context">
    <h1> You are epic!</h1>
  </div>

  <div class="context2">
    <router-link to="/categoryselection" class="button" style="align-self: center; padding: 17.5px; ">Beginne deine Reise
    </router-link>
  </div>
  <!--<router-link to="/categoryselection" class="button" style="align-self: center; word-wrap: break-word; width: 30%">Beginne deine Reise
  </router-link>-->
  <div class="area" >
    <ul class="circles">
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
      <li></li>
    </ul>
  </div >

</template>

<script>
export default {
  name: "HomePage"
}
</script>


<style scoped lang="scss">
@import url('https://fonts.googleapis.com/css?family=Jost');


.page{
  margin: 0px !important;
  padding: 0px !important;
}

.context{
  width: 100%;
  position: absolute;
  left: 50%;
  transform: translate(-50%, 0);
  bottom: 50%;
  z-index: 2;
}

.context2{
  position: absolute;
  top:50vh;
  z-index: 2;
  font-family: Jost;
  font-size: clamp(0.5rem,2.5vw,1.5rem);
  //text-align: center;
  white-space: nowrap;
  left: 50%;
  transform: translate(-50%, 10%);
}

.context h1{
  text-align: center;
  color: #3A93A7;
  font-size: clamp(2.5rem,7vw,4.5rem);
  font-family: Jost;
  font-style: normal;
  font-weight: bold;
  text-transform: uppercase;
}
  .area{
    background: #FFFFFF;
    background: -webkit-linear-gradient(to left, #BA2660, #FFFFFF);
    width: 100%;
    height:100vh;
  }


.circles{
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  overflow: hidden;
  padding: 0;
  margin: 0;
}

.circles li{
  position: absolute;
  display: block;
  list-style: none;
  width: 20px;
  height: 20px;
  background: rgba(186, 38, 96);
  animation: animate 25s linear infinite;
  bottom: -150px;

}

.circles li:nth-child(1){
  left: 25%;
  width: 80px;
  height: 80px;
  animation-delay: 0s;
}


.circles li:nth-child(2){
  left: 10%;
  width: 20px;
  height: 20px;
  animation-delay: 2s;
  animation-duration: 12s;
}

.circles li:nth-child(3){
  left: 70%;
  width: 20px;
  height: 20px;
  animation-delay: 4s;
}

.circles li:nth-child(4){
  left: 40%;
  width: 60px;
  height: 60px;
  animation-delay: 0s;
  animation-duration: 18s;
}

.circles li:nth-child(5){
  left: 65%;
  width: 20px;
  height: 20px;
  animation-delay: 0s;
}

.circles li:nth-child(6){
  left: 75%;
  width: 110px;
  height: 110px;
  animation-delay: 3s;
}

.circles li:nth-child(7){
  left: 35%;
  width: 150px;
  height: 150px;
  animation-delay: 7s;
}

.circles li:nth-child(8){
  left: 50%;
  width: 25px;
  height: 25px;
  animation-delay: 15s;
  animation-duration: 45s;
}

.circles li:nth-child(9){
  left: 20%;
  width: 15px;
  height: 15px;
  animation-delay: 2s;
  animation-duration: 35s;
}

.circles li:nth-child(10){
  left: 85%;
  width: 150px;
  height: 150px;
  animation-delay: 0s;
  animation-duration: 11s;
}



@keyframes animate {

  0%{
    transform: translateY(0) rotate(0deg);
    opacity: 1;
    border-radius: 0;
  }

  100%{
    transform: translateY(-1000px) rotate(720deg);
    opacity: 0;
    border-radius: 50%;
  }

}
</style>