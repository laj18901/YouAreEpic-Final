<template>

    <ngoitemdetailed :logo=ngo.logoLink :name=ngo.name :description=ngo.description :short-description="ngo.shortDescription" :id=ngo.id></ngoitemdetailed>
  <div class="container">
      <div class="buttonwrapper">
        <button class="moneyButton" @click="showDialogFunc(1)">1€</button>
        <button class="moneyButton" @click="showDialogFunc(2)">2€</button>
        <button class="moneyButton" @click="showDialogFunc(5)">5€</button>
        <button class="moneyButton" @click="showDialogFunc(10)">10€</button>
      </div>

      <div class="textfieldwrapper">
          <label class="label">Eigenen Betrag</label>
          <input class="textfield" v-model="amountInput" min="1" max="9999" type="number" name="number" placeholder="100€" />

      </div>
    <button type="submit" class="submitButton" @click="showDialogFunc(amountInput)" :disabled="amountInput === 0">Spenden</button>
</div>

  <dialog class="dialog" v-if="showDialog" open>
    <p>Wollen Sie wirklich {{amount}}€ spenden ?</p>
    <div style="display: flex; justify-content: center">
    <button class="dialogButton" @click="showDialog = false">Nein</button>
    <button class="dialogButton" @click="submit">Ja</button>
    </div>
  </dialog>

</template>

<script>
import ngoitemdetailed from '../components/NGOItemDetailed.vue'
import axios from 'axios'
export default {
  name: 'Payment',
  components: {
    ngoitemdetailed
  },
  methods: {
    showDialogFunc(number){

      this.amount = number
      this.showDialog = true
    },
    async submit(){
      let tempngoid = this.id;
      let tempmoney = this.amount;
      await fetch("/api/payment/create-checkout-session", {
        headers: [
            [ "Content-Type", "application/json" ]
        ],
        method:"POST",
        body:JSON.stringify({
          ngoid:tempngoid,
          money: tempmoney
        }),

      })
      .then(response => response.json()).then(r => window.location.href = r.url)
    }
  },
  mounted () {
    axios.get(`/api/npos/${this.id}`)
      .then(response => (this.ngo = response.data))
  },

  created () {
    let x = this.$route.params
    this.id = x['ngoid']
  },

  data () {
    return {
      id: '',
      ngo: {},
      customAmount: false,
      showDialog: false,
      amount: 0,
      amountInput: 0,
    }
  }

}

</script>

<style scoped>
@import url('https://fonts.googleapis.com/css?family=Jost');
.label{
  font-family: Jost;
  font-style: normal;
  font-size: clamp(0.85rem, 3rem, 1rem);
}


.container{
  display: grid;
  grid-template-columns: 1fr;
  grid-auto-flow: row;
  grid-template-rows: 1fr auto 1fr;
  justify-content: center;
  justify-items: center;
  align-items: baseline;
  align-content: center;
  gap: 1rem;

}

.buttonwrapper{
  margin-top: 100px;
  justify-content: center;
  display: flex;
}


</style>
