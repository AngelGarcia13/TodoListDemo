# TodoListDemo

https://www.canva.com/design/DAFpEa5SV0k/tpcog5WrsIWWn5raF_euaw/edit?utm_content=DAFpEa5SV0k&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton

In this enlightening talk, we will provide a comprehensive overview of ephemeral environments and their integration within the CI pipeline. We will explore their transformative impact on end-to-end systems testing and discuss how they enable developers to catch and address potential problems even before code is merged into the main branch. By leveraging ephemeral environments effectively, organizations can enhance their CI process and foster a culture of quality and reliability.

To see the demo in action, please run:

```console
docker compose build --no-cache
docker compose up --force-recreate

//Run the API Tests
docker run --rm --network host --name api-integration-tests -v "$(pwd)/TodoListApiTests/postman:/etc/newman" -t postman/newman run \
    TodoListApi.postman_collection.json \
    --env-var "baseUrl=http://localhost:8080" \
    --verbose --insecure \
    --reporters cli,junit \
    --reporter-junit-export result/api-tests-results.xml
```
