FROM node:23.10.0
WORKDIR /app

# Copy package files first for better layer caching
COPY package*.json ./
RUN npm install

# Copy application code
COPY . .

# Start the application
CMD ["node", "index.js"]