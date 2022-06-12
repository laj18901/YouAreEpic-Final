import{_ as u}from"./index.499fa5c3.js";import{o as l,c as d,a as t,t as r,f as p,g,m as h,v as _,l as f,F as v,p as y,h as b}from"./vendor.260b2556.js";import{a as w}from"./index.fa753d9e.js";import"./axios.c5189851.js";const D={name:"ngoitemdetailed",props:{logo:String,name:String,description:String,shortDescription:String,website:String,id:String}},S={class:"wrapperDetailed"},k={class:"itemDetailed"},x=["src"],B={class:"ngoname"},C={class:"detailedDetails"},I={class:"detailedDetail"};function F(o,e,a,c,n,i){return l(),d("div",S,[t("div",k,[t("img",{src:a.logo,alt:"",class:"ngologo"},null,8,x),t("span",B,r(a.name),1)]),t("div",C,[t("p",I,r(a.shortDescription),1)])])}var N=u(D,[["render",F]]);const P={name:"Payment",components:{ngoitemdetailed:N},methods:{showDialogFunc(o){this.amount=o,this.showDialog=!0},async submit(){this.id,this.amount,await fetch("/api/payment/create-checkout-session",{headers:[["Content-Type","application/json"]],method:"POST",body:JSON.stringify({ngoid:this.id,money:this.amount})}).then(o=>o.json()).then(o=>window.location.href=o.url)}},mounted(){w.get(`/api/npos/${this.id}`).then(o=>this.ngo=o.data)},created(){let o=this.$route.params;this.id=o.ngoid},data(){return{id:"",ngo:{},customAmount:!1,showDialog:!1,amount:0,amountInput:0}}},V=o=>(y("data-v-86efaf06"),o=o(),b(),o),j={class:"container"},T={class:"buttonwrapper"},E={class:"textfieldwrapper"},J=V(()=>t("label",{class:"label"},"Eigenen Betrag",-1)),O=["disabled"],$={key:0,class:"dialog",open:""},A={style:{display:"flex","justify-content":"center"}};function L(o,e,a,c,n,i){const m=p("ngoitemdetailed");return l(),d(v,null,[g(m,{logo:n.ngo.logoLink,name:n.ngo.name,description:n.ngo.description,"short-description":n.ngo.shortDescription,id:n.ngo.id},null,8,["logo","name","description","short-description","id"]),t("div",j,[t("div",T,[t("button",{class:"moneyButton",onClick:e[0]||(e[0]=s=>i.showDialogFunc(1))},"1\u20AC"),t("button",{class:"moneyButton",onClick:e[1]||(e[1]=s=>i.showDialogFunc(2))},"2\u20AC"),t("button",{class:"moneyButton",onClick:e[2]||(e[2]=s=>i.showDialogFunc(5))},"5\u20AC"),t("button",{class:"moneyButton",onClick:e[3]||(e[3]=s=>i.showDialogFunc(10))},"10\u20AC")]),t("div",E,[J,h(t("input",{class:"textfield","onUpdate:modelValue":e[4]||(e[4]=s=>n.amountInput=s),min:"1",max:"9999",type:"number",name:"number",placeholder:"100\u20AC"},null,512),[[_,n.amountInput]])]),t("button",{type:"submit",class:"submitButton",onClick:e[5]||(e[5]=s=>i.showDialogFunc(n.amountInput)),disabled:n.amountInput===0},"Spenden",8,O)]),n.showDialog?(l(),d("dialog",$,[t("p",null,"Wollen Sie wirklich "+r(n.amount)+"\u20AC spenden ?",1),t("div",A,[t("button",{class:"dialogButton",onClick:e[6]||(e[6]=s=>n.showDialog=!1)},"Nein"),t("button",{class:"dialogButton",onClick:e[7]||(e[7]=(...s)=>i.submit&&i.submit(...s))},"Ja")])])):f("",!0)],64)}var z=u(P,[["render",L],["__scopeId","data-v-86efaf06"]]);export{z as default};
