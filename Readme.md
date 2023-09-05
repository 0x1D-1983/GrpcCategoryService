# Grpc Category Service

This is a demo of a grpc service exposed through an Envoy proxy. Both services are deployed to a Kubernetes cluster using Skaffold. The cluster also includes a Postgres database serving the grpc service and a Weather API http service for variety, demonstrating different routing features of Envoy.

The repo includes the following services:
1. Envoy
2. Postgres
3. Grpc Category Api
4. Weather Api 