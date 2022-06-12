<template>
  <main class="page-centered">
    <div class="card card-shadow login-card">

      <h1>Login</h1>
            
      <div class="login-links">
        <a class="btn btn-main btn-discord" href="/api/oauth1/login">
          Log in
        </a>
      </div>

      <p>You will be redirected to Twiiter to authenticate.</p>
      
    </div>
  </main>
</template>

<script>

export default {
  name: "LoginPage"
}
</script>

<style scoped lang="scss">
.login-card {
  max-width: 30rem;
  margin-inline: auto;
  
  h1 {
    text-align: center;
    margin-bottom: 1rem;
    color: var(--color-main);
    font-size: 2.4em;
  }
  > p {
    text-align: center;
    color: var(--scheme-shade-2);
    font-size: 0.9em;
    margin-bottom: 0;
  }
  .login-links {
    display: flex;
    flex-direction: column;
    align-items: stretch;
    margin-top: 1rem;
    margin-bottom: 1rem;
    
    gap: 0.5rem;
    
    > a, p {
      text-align: center;
    }
    
    a:not(.btn) {
      color: var(--color-main);
    }
  }
}
.btn-discord {
  width: 100%;
  --btn-color: #5865f2;
  display: flex;
  justify-content: center;
  
  color: var(--scheme-foreground);
  font-size: 1.15em;
  img {
    height: 5ch;
  }
}
</style>