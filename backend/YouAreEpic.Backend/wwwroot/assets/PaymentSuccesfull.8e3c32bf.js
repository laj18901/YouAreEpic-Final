import{_ as c}from"./index.278ab0a0.js";import{o as r,c as i,a as o,p as d,h as u}from"./vendor.260b2556.js";const p={name:"PaymentSuccesfull",data(){return{ngoid:null,ammount:0}},methods:{openPost(){this.$router.push(`/post?ngo=${this.ngoid}&ammount=${this.ammount}`)}},mounted(){let t=this.$route.query.session_id;fetch(`/api/payment/checkout-session?sessionid=${t}`).then(e=>e.json()).then(e=>{console.log(e),this.ngoid=e.ngoid,this.ammount=parseFloat(e.ammount)/100}).catch(e=>console.log(e))}},n=t=>(d("data-v-2944373a"),t=t(),u(),t),l={class:"wrapper"},m=n(()=>o("h1",{class:"header"},"DANKE!",-1)),_=n(()=>o("p",{class:"text"},"Die Spende wurde erfolgreich durchgef\xFChrt!",-1));function h(t,e,f,g,y,s){return r(),i("div",l,[m,_,o("button",{onClick:e[0]||(e[0]=(...a)=>s.openPost&&s.openPost(...a)),class:"button",style:{"align-self":"center","word-wrap":"break-word",width:"50%"}},"Teile jetzt deinen Epic Moment mit deiner Community ")])}var S=c(p,[["render",h],["__scopeId","data-v-2944373a"]]);export{S as default};
