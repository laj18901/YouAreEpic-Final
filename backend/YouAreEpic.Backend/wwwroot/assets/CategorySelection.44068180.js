import{_ as d}from"./index.08602313.js";import{o as a,c as l,a as o,t as g,n as p,f as m,F as u,j as h,p as f,h as y,k as v}from"./vendor.260b2556.js";import{a as C}from"./index.522124fa.js";import"./axios.d8123761.js";const x={name:"Card",props:{source:String,title:String},data(){return{show:!1}},computed:{filterStyle(){return this.show?{border:"none",borderRadius:"2pt",boxShadow:"0 0 0 2pt #3A93A7",outline:"none",transition:".1s",backgroundColor:"#E8F1F3"}:""}}},b={class:"title"},k=["src"];function S(e,s,i,_,n,r){return a(),l("button",{class:"card",style:p(r.filterStyle),onClick:s[0]||(s[0]=c=>n.show=!n.show)},[o("span",b,g(i.title),1),o("img",{src:i.source,alt:"Tier",class:"categoryImage"},null,8,k)],4)}var w=d(x,[["render",S]]);const A={name:"App",components:{Card:w},data(){return{categories:[],selectedCategories:[],user:""}},methods:{nextPage(){let e=this.selectedCategories;this.$router.push({name:"NGOList",query:{categories:e}})}},mounted(){C.get("api/categories").then(e=>{this.categories=e.data.categories}).catch(e=>console.log(e))}},I=e=>(f("data-v-e054e340"),e=e(),y(),e),P={class:"categories"},B={class:"buttons"},F=I(()=>o("br",null,null,-1));function $(e,s,i,_,n,r){const c=m("card");return a(),l(u,null,[o("div",P,[(a(!0),l(u,null,h(n.categories,t=>(a(),v(c,{key:t.id,onClick:L=>n.selectedCategories.push(t.id),title:t.name,source:t.imageLink},null,8,["onClick","title","source"]))),128))]),o("div",B,[o("button",{class:"button",style:{width:"40%"},onClick:s[0]||(s[0]=(...t)=>r.nextPage&&r.nextPage(...t))},"Auswahl best\xE4tigen"),F,o("button",{class:"button",style:{width:"40%"},onClick:s[1]||(s[1]=(...t)=>r.nextPage&&r.nextPage(...t))},"Alles Anzeigen ")])],64)}var q=d(A,[["render",$],["__scopeId","data-v-e054e340"]]);export{q as default};