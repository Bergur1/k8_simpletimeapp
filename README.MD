Project that demonstrates blue-green deployment of a simple webapp running in kubernetes.

Depends on:
Docker-Windows with kubernetes enabled
* Helm (https://helm.sh/docs/intro/install/)
* Dotnet sdk (if not building app in container https://dotnet.microsoft.com/en-us/download)
* Haproxy Gateway Api (https://haproxy-ingress.github.io/docs/configuration/gateway-api/)


Create the docker image, only used locally so its important to tag them

```docker build . --pull -t timeapp:v1```

Open up Program.cs and make any changes you'd like, I'm updating the response to include the hostname.

```docker build . --pull -t timeapp:v2```

Run v1 of the app:
```kubectl apply -f timeappblue.yaml```

Ensure Kubernetes is up and running
Install Haproxy-Ingress
```helm install haproxy-ingress haproxy-ingress/haproxy-ingress --create-namespace --namespace ingress-controller --version 0.14.2 -f haproxy-ingress-values.yaml```

Install the gateway-api libraries:
```kubectl apply -f https://github.com/kubernetes-sigs/gateway-api/releases/download/v0.6.2/standard-install.yaml```

Setup the GatewayClass
```kubectl apply -f haproxy-gateway.yaml```

Add the HTTPRoute, this is where the important rules get updated during deploy
```kubectl apply -f timerouteblue.yaml```

In a separate terminal that the app is running and returning a response
```bash ./pingtest.sh 5000```

Deploy the green app
```kubectl apply -f timeappgreen.yaml```

Add the HTTPRoute, this is where the important rules get updated during deploy
```kubectl apply -f timeroutegreen.yaml```

optionally if you want to see traffic hit both servers use the bluegreen routes
Add the HTTPRoute, this is where the important rules get updated during deploy
```kubectl apply -f timeroutebluegreen.yaml```


Todo:
 - use/create request app that can burst X times per second
 - run reqest app in docker and resolve route using gateway




