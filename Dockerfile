FROM node:23.10.0
WORKDIR /app

# Copy package files first for better layer caching
COPY package*.json ./
RUN npm install

# Copy application code
COPY . .
ENV MJ_APIKEY_PUBLIC=486b26999cf0f5fba5b98c3aeeb4c4b8
ENV MJ_APIKEY_PRIVATE=e1d7f36304b6581ca81548b10967a33c
ENV RECIPIENT_EMAIL=pithonkids@gmail.com
ENV RECIPIENT_NAME=PithonKids
ENV PORT=8080
EXPOSE 8080

# Start the application
CMD ["node", "/app/index.js"]
