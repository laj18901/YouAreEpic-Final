import{a as m}from"./index.522124fa.js";import{o as i,c as u,a,m as p,v as c,l as f,F as g,p as h,h as x}from"./vendor.260b2556.js";import{_}from"./index.08602313.js";import"./axios.d8123761.js";const v={name:"Post",methods:{countChar(){document.querySelector("textarea").addEventListener("input",({currentTarget:t})=>{t.getAttribute("maxlength");const n=t.value.length;document.getElementById("current").innerText=n})},loadFile(e){console.log(e),Array.from(e.target.files).forEach(t=>{var n=document.getElementById("output");const l=document.createElement("img");l.src=URL.createObjectURL(t),n.appendChild(l)}),this.files=Array.from(e.target.files)},async submit(){var e=new FormData;this.files.forEach(t=>{e.append("files",t)}),e.append("text",this.textInput),e.append("ammount",this.ammount),e.append("ngoid",this.ngoid),m.post("/api/twitter/tweet",e,{headers:{"Content-Type":"multipart/form-data","Access-Control-Allow-Origin":"*"}}).then(t=>{var n=t.data;this.$router.push({name:"PostSuccess",query:n})}).catch(t=>console.error(t))}},mounted(){this.ammount=this.$route.query.ammount,this.ngoid=this.$route.query.ngo},data(){return{ammount:0,ngoid:null,file:[],text:0,textInput:null,fileUploaded:!1}}},d=e=>(h("data-v-28428462"),e=e(),x(),e),y=d(()=>a("div",{style:{"margin-bottom":"10px"}},[a("span",{id:"current"},"0 "),a("span",{id:"maximum"},"/ 280")],-1)),I={class:"submitWrapper"},b=d(()=>a("label",{for:"file-upload",class:"custom-file-upload"}," Bild / Video hochladen ",-1)),C={key:0,id:"output",class:"ImageContainer"},w={class:"wrap"};function B(e,t,n,l,s,r){return i(),u(g,null,[a("div",null,[a("label",null,[p(a("textarea",{class:"textarea",onInput:t[0]||(t[0]=(...o)=>r.countChar&&r.countChar(...o)),"onUpdate:modelValue":t[1]||(t[1]=o=>s.textInput=o),maxlength:"280",placeholder:"Text"},`\r
    `,544),[[c,s.textInput]])]),y]),a("div",I,[b,a("input",{id:"file-upload",onClick:t[2]||(t[2]=o=>s.fileUploaded=!0),enctype:"multipart/form-data",type:"file",accept:"image/gif,image/png,image/jpeg,image/webp,video/mp4",multiple:"",onChange:t[3]||(t[3]=(...o)=>r.loadFile&&r.loadFile(...o))},null,32),s.fileUploaded?(i(),u("div",C)):f("",!0)]),a("div",w,[a("button",{class:"dialogButton",onClick:t[4]||(t[4]=(...o)=>r.submit&&r.submit(...o))},"Tweet posten")])],64)}var k=_(v,[["render",B],["__scopeId","data-v-28428462"]]);export{k as default};