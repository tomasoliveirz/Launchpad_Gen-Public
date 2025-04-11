FROM node:20
WORKDIR /app
RUN npm install express body-parser solc
COPY . .
EXPOSE 3000
CMD ["node", "index.js"]