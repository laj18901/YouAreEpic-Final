import { reactive} from "vue";
import { routerKey } from "vue-router";

export class Auth {
    profile;
   authenticated;
}

export class AuthProfile {
    
    name;
    email;
    
    constructor(profile) {
        this.name = profile.name;
        this.email = profile.email;
    }
    
}

const auth = reactive(new Auth());

let router;
function checkAuthorization() {
    fetch('/api/oauth1/me',{
        headers:{
            'Access-Control-Allow-Origin': '*'
            }
        })
        .then(value => {
            auth.authenticated = value.status === 200;
            if(auth.authenticated) {
                value.json().then((prof) => {
                    auth.profile = new AuthProfile(prof);
                });

                var returnUrl = sessionStorage.getItem('returnUrl');
                if(returnUrl){
                    sessionStorage.removeItem('returnUrl');
                    router.push(returnUrl);
                }
               
            }
        })
        .catch(reason => {
            auth.authenticated = false;
        });
}

const authPlugin = {
    install: (app) => {
        checkAuthorization();
        
        app.provide("auth", auth);
        router = app.config.globalProperties.$router;
    }
};

export default authPlugin;

export function isAuthenticated() {
    return auth.authenticated;
}