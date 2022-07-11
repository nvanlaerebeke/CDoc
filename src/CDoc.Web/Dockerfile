FROM node:current-alpine AS build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

FROM httpd:latest
COPY --from=build /usr/src/app/dist/cdoc.web /usr/local/apache2/htdocs/