<template>
  <div>
  <label>
    <textarea class="textarea" @input="countChar" v-model="textInput" maxlength="280" placeholder="Text">
    </textarea>

  </label>
  <div style="margin-bottom: 10px">
    <span id="current">0 </span>
    <span id="maximum">/ 200</span>
  </div>
  </div>

  <div class="submitWrapper">
  <label for="file-upload" class="custom-file-upload">
   Bild / Video hochladen
  </label>
  <input id="file-upload" v-on:click="fileUploaded = true"  enctype="multipart/form-data" type="file" accept="image/gif,image/png,image/jpeg,image/webp,video/mp4" multiple v-on:change="loadFile">
    <div id="output" v-if="fileUploaded" class="ImageContainer"></div>
  </div>

  <div class="wrap">
    <button class="dialogButton" @click="submit">Tweet posten</button>
  </div>
</template>

<script>
import axios from 'axios';
export default {

  name: "Post",
  methods: {
    countChar() {
      const textarea = document.querySelector("textarea");

      textarea.addEventListener("input", ({currentTarget: target}) => {
        const maxLength = target.getAttribute("maxlength");
        const currentLength = target.value.length;

        document.getElementById('current').innerText = currentLength;

      });
    },
    loadFile (event) {
      console.log(event)
      Array.from(event.target.files).forEach(file => {
         var output = document.getElementById('output');
         const img = document.createElement("img");
          img.style = 'border-radius: 10%;\n' +
              '  aspect-ratio: 1/1;\n' +
              '  height: clamp(100px,30vw,200px);\n' +
              '  margin: 5px;\n' +
              '  object-fit: cover;';
          img.src = URL.createObjectURL(file);
         output.appendChild(img);
         
      });
      this.files = Array.from(event.target.files);
      
     },
    async submit(){
      var formData = new FormData();
    
      this.files.forEach(file=>{
        formData.append("files", file);
      });

      formData.append("text", this.textInput);
      formData.append("ammount",this.ammount);
      formData.append("ngoid",this.ngoid);
      axios.post('/api/twitter/tweet', formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
            'Access-Control-Allow-Origin': '*'
          }
      }).then(result=> {
        var tweetdata = result.data;
          this.$router.push({ name: 'PostSuccess', query: tweetdata })
      })
      .catch(e => console.error(e));
    }
  },
  mounted(){
    this.ammount = this.$route.query.ammount;
    this.ngoid = this.$route.query.ngo;
  },
  data () {
    return {
      ammount: 0,
      ngoid:null,
      file: [],
      text: 0,
      textInput: null,
      fileUploaded: false
    }
  }
};




</script>

<style scoped>

.ImageContainer{
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-auto-flow: row;
  grid-template-rows: 1fr 1fr;
  gap: clamp(0.25rem,1vw,1rem);
  justify-items: center;
  align-items: center;
  justify-content: center;
  align-content: center;
  margin-top: 30px;
}


#output{
  border-radius: 10%;
  width: 20vh;
  height: 20vh;
  object-fit: cover;
  border: none;
}


.submitWrapper{
  display: grid;
  place-content: center;
  justify-content: center;
  align-items: center;
  justify-items: center;
}

.wrap{
  position: absolute;
  left: 50%;
  transform: translate(-50%, 0);
  bottom: 5vh;
}

</style>