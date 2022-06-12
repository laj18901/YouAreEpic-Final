var v=Object.defineProperty;var P=(e,t,o)=>t in e?v(e,t,{enumerable:!0,configurable:!0,writable:!0,value:o}):e[t]=o;var l=(e,t,o)=>(P(e,typeof t!="symbol"?t+"":t,o),o);import{o as p,c as _,a as u,t as I,r as L,b as S,d as A,e as O}from"./vendor.260b2556.js";const $=function(){const t=document.createElement("link").relList;if(t&&t.supports&&t.supports("modulepreload"))return;for(const r of document.querySelectorAll('link[rel="modulepreload"]'))n(r);new MutationObserver(r=>{for(const s of r)if(s.type==="childList")for(const i of s.addedNodes)i.tagName==="LINK"&&i.rel==="modulepreload"&&n(i)}).observe(document,{childList:!0,subtree:!0});function o(r){const s={};return r.integrity&&(s.integrity=r.integrity),r.referrerpolicy&&(s.referrerPolicy=r.referrerpolicy),r.crossorigin==="use-credentials"?s.credentials="include":r.crossorigin==="anonymous"?s.credentials="omit":s.credentials="same-origin",s}function n(r){if(r.ep)return;r.ep=!0;const s=o(r);fetch(r.href,s)}};$();const x="modulepreload",m={},b="/",a=function(t,o){return!o||o.length===0?t():Promise.all(o.map(n=>{if(n=`${b}${n}`,n in m)return;m[n]=!0;const r=n.endsWith(".css"),s=r?'[rel="stylesheet"]':"";if(document.querySelector(`link[href="${n}"]${s}`))return;const i=document.createElement("link");if(i.rel=r?"stylesheet":x,r||(i.as="script",i.crossOrigin=""),i.href=n,document.head.appendChild(i),r)return new Promise((y,E)=>{i.addEventListener("load",y),i.addEventListener("error",E)})})).then(()=>t())};var h=(e,t)=>{const o=e.__vccOpts||e;for(const[n,r]of t)o[n]=r;return o};const k={name:"Seperator",props:{text:String}},B={style:{width:"100%",height:"50%","max-height":"10rem","border-bottom":"1px solid black","text-align":"center"}},R={style:{},class:"seperator"};function D(e,t,o,n,r,s){return p(),_("div",B,[u("span",R,I(o.text),1)])}var V=h(k,[["render",D],["__scopeId","data-v-2c8d3fb6"]]);const T={name:"Stepper",created(){switch(this.$route.meta.step){case 1:document.getElementById("#first").classList.add("current");break;case 2:document.getElementById("#first").classList.add("complete"),document.getElementById("#second").classList.add("current");break;case 3:document.getElementById("#first").classList.add("complete"),document.getElementById("#second").classList.add("complete"),document.getElementById("#third").classList.add("current");break}}},w={class:"stepper"},N=u("li",{id:"first",class:"stepper__item"},"Interessen",-1),W=u("li",{id:"second",class:"stepper__item"},"NGOs",-1),q=u("li",{id:"third",class:"stepper__item"},"Bezahlen",-1),C=u("li",{id:"fourth",class:"stepper__item"},"Post",-1),F=[N,W,q,C];function G(e,t,o,n,r,s){return p(),_("ul",w,F)}var z=h(T,[["render",G]]);class H{constructor(){l(this,"profile");l(this,"authenticated")}}class U{constructor(t){l(this,"name");l(this,"email");this.name=t.name,this.email=t.email}}const c=L(new H);let f;function j(){fetch("/api/oauth1/me",{headers:{"Access-Control-Allow-Origin":"*"}}).then(e=>{if(c.authenticated=e.status===200,c.authenticated){e.json().then(o=>{c.profile=new U(o)});var t=sessionStorage.getItem("returnUrl");t&&(sessionStorage.removeItem("returnUrl"),f.push(t))}}).catch(e=>{c.authenticated=!1})}const K={install:e=>{j(),e.provide("auth",c),f=e.config.globalProperties.$router}};function J(){return c.authenticated}const d=S({components:{Seperator:V,Stepper:z}}),g=A({history:O(),routes:[{path:"/",name:"HomePage",component:()=>a(()=>import("./HomePage.866379e6.js"),["assets/HomePage.866379e6.js","assets/HomePage.bd7cf2ee.css","assets/vendor.260b2556.js"]),meta:{firstPage:!0}},{path:"/login",component:()=>a(()=>import("./LoginPage.b79d83e2.js"),["assets/LoginPage.b79d83e2.js","assets/LoginPage.cdf326f5.css","assets/vendor.260b2556.js"])},{path:"/categoryselection",name:"CategorySelection",component:()=>a(()=>import("./CategorySelection.97afda0c.js"),["assets/CategorySelection.97afda0c.js","assets/CategorySelection.6e2ef871.css","assets/vendor.260b2556.js","assets/index.fa753d9e.js","assets/axios.c5189851.js"]),meta:{text:"W\xE4hlen Sie Ihre Interessen",step:1}},{path:"/ngolist",name:"NGOList",component:()=>a(()=>import("./NGOList.b77c2818.js"),["assets/NGOList.b77c2818.js","assets/NGOList.c13a3191.css","assets/vendor.260b2556.js","assets/axios.c5189851.js"]),meta:{text:"W\xE4hlen Sie eine NGO",step:2}},{path:"/payment/:ngoid",name:"Payment",component:()=>a(()=>import("./Payment.4244288b.js"),["assets/Payment.4244288b.js","assets/Payment.5ad06d8c.css","assets/vendor.260b2556.js","assets/index.fa753d9e.js","assets/axios.c5189851.js"]),meta:{text:"W\xE4hlen Sie einen Betrag",step:3}},{path:"/payment/error",name:"PaymentFailed",component:()=>a(()=>import("./PaymentFailed.63d52f7c.js"),["assets/PaymentFailed.63d52f7c.js","assets/PaymentFailed.36e19c8f.css","assets/vendor.260b2556.js"]),meta:{text:"Fehlgeschlagen"}},{path:"/payment/success",name:"PaymentSuccess",component:()=>a(()=>import("./PaymentSuccesfull.67f5654d.js"),["assets/PaymentSuccesfull.67f5654d.js","assets/PaymentSuccesfull.94e99258.css","assets/vendor.260b2556.js"]),meta:{text:"Erfolgreich"}},{path:"/post",name:"post",component:()=>a(()=>import("./Post.056e3caf.js"),["assets/Post.056e3caf.js","assets/Post.69d6bf51.css","assets/index.fa753d9e.js","assets/axios.c5189851.js","assets/vendor.260b2556.js"]),meta:{text:"Post erstellen",requireAuthentication:!0}},{path:"/PostSuccess",name:"PostSuccess",component:()=>a(()=>import("./TweetSuccesfull.359d538f.js"),["assets/TweetSuccesfull.359d538f.js","assets/TweetSuccesfull.bcb35fdc.css","assets/vendor.260b2556.js","assets/axios.c5189851.js"]),meta:{text:"Fertig"}}]});g.beforeEach((e,t,o)=>{e.meta.requireAuthentication?J()?o():(sessionStorage.setItem("returnUrl",e.fullPath),o({path:"/login"})):o()});d.use(g);d.use(K);d.mount("#app");export{h as _};
