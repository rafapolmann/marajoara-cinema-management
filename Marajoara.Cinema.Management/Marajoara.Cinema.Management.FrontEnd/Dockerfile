FROM node:18-alpine as build-step
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build --prod

FROM nginx
COPY --from=build-step /app/dist/marajoara.cinema.management.front-end /usr/share/nginx/html
EXPOSE 4200:80
