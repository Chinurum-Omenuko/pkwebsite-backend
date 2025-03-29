FROM node:23.10.0
WORKDIR /app
COPY package*.json ./
RUN ls -la
COPY . .
RUN ls -la
RUN pwd
RUN cat index.js
RUN npm install
CMD ["node", "index.js"]