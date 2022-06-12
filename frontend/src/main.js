// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import { createApp } from 'vue/dist/vue.esm-bundler'
import './assets/css/main.scss'
import {createRouter, createWebHistory} from "vue-router";
import Seperator from './components/Seperator.vue'
import Stepper from "./components/Stepper.vue";
import authPlugin from "./plugins/auth-plugin";
import {isAuthenticated} from "./plugins/auth-plugin";


/* eslint-disable no-new */
const app = createApp({
  components: {
    Seperator,
    Stepper
  }
});

const router = createRouter({
  history: createWebHistory(),
  /* base: '/YouAreEpic', */
  routes: [
    {
      path: "/",
      name: 'HomePage',
      component: () => import("./pages/HomePage.vue"),
      meta: {
        firstPage: true
      }
    },
    {
      path: '/login',
      component: () => import('./pages/LoginPage.vue'),
    },
    {
      path: '/categoryselection',
      name: 'CategorySelection',
      component: () => import('./pages/CategorySelection.vue'),
      meta: {
        text: 'Wählen Sie Ihre Interessen',
        step: 1
      }
    },
    {
      path: '/ngolist',
      name: 'NGOList',
      component: () => import('./pages/NGOList.vue'),
      meta: {
        text: 'Wählen Sie eine NGO',
        step: 2
      }
    },
    {
      path: '/payment/:ngoid',
      name: 'Payment',
      component: () => import('./pages/Payment.vue'),
      meta: {
        text: 'Wählen Sie einen Betrag',
        step: 3
      }
    },
    {
      path: '/payment/error',
      name: 'PaymentFailed',
      component: () => import('./pages/PaymentFailed.vue'),
      meta: {
        text: 'Fehlgeschlagen'
      }
    },
    {
      path: '/payment/success',
      name: 'PaymentSuccess',
      component: () => import('./pages/PaymentSuccesfull.vue'),
      meta: {
        text: 'Erfolgreich'
      }
    },
    {
      path: '/post',
      name: 'post',
      component: () => import('./pages/Post.vue'),
      meta: {
        text: 'Post erstellen',
        requireAuthentication: true
      }
    },
    {
      path: '/PostSuccess',
      name: 'PostSuccess',
      component: () => import('./pages/TweetSuccesfull.vue'),
      meta: {
        text: 'Fertig'
      }
    }
  ],
});

router.beforeEach((to, from, next) => {

  if(to.meta.requireAuthentication) {
      if(isAuthenticated()) {
       next();
      } else {
          sessionStorage.setItem('returnUrl', to.fullPath);
          next({ path: "/login" });
      }
  } else {
      next();
  }
});

app.use(router);
app.use(authPlugin);
app.mount('#app');
