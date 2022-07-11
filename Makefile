.PHONY: container-api container-web api web push push-web push-api

PWD:=$(shell pwd)
USER:=crazytje
VERSION=$(shell cat VERSION)
PROJECT_WEB=cdoc.web
PROJECT_API=cdoc.api

container-api:
	cd src && \
	docker build \
		-f Dockerfile \
		-t ${USER}/${PROJECT_API}:${VERSION} \
		.
	docker tag ${USER}/${PROJECT_API}:${VERSION} ${USER}/${PROJECT_API}:latest

container-web:
	cd src/CDoc.Web && \
	docker build \
		-f Dockerfile \
		-t ${USER}/${PROJECT_WEB}:${VERSION} \
		.
	docker tag ${USER}/${PROJECT_WEB}:${VERSION} ${USER}/${PROJECT_WEB}:latest


api:
	docker run \
		--name "cdoc.api" \
		-v "${PWD}/etc/cdoc.json:/etc/cdoc.json" \
		-p 4201:80 \
		"cdoc.api" 

web:
	docker run \
		--name "cdoc.api" \
		-v "${PWD}/etc/cdoc.json:/etc/cdoc.json" \
		-p 4201:80 \
		"cdoc.api" 

push-web:
	docker push ${USER}/${PROJECT_WEB}:latest
	docker push ${USER}/${PROJECT_WEB}:${VERSION}

push-api:
	docker push ${USER}/${PROJECT_API}:latest
	docker push ${USER}/${PROJECT_API}:${VERSION}
