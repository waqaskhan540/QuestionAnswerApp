FROM node:12.2.0-alpine

RUN mkdir /app
# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
# install and cache app dependencies
COPY . .
RUN ls
RUN rm -rf package-lock.json
RUN rm -rf node_modules && npm install 

EXPOSE 3000
# start app
CMD ["npm", "start"]