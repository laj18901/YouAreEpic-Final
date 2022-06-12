# YouAreEpic-Final

# Zugang zu Azure,Stripe,Twitter

Unter "appsettings.Development.json" die Keys dann mit euren generierten Keys austauschen


# Node-Modules runterladen

```console
npm install
npm update
```

# Dev-Env
Zurzeit ist das Projekt auf ein Development-Env aufgesetzt. Um dieses auszuführen folgenden Command im Frontend-Ordner ausführen:

```console
npm run dev
```

Danach Backend in der IDE-Ausführen

# Production-Env

Um auf Production umzustellen einfach im Backend unter "appsettings.Development.json":

1. "FrontEndDomain" löschen
2. "Stripe" : "Domain" auf PublicDomain stellen

Danach das Frontend aufs Backend builden: 

```console
npm run build
```

Und das Backend dann ausführen.


