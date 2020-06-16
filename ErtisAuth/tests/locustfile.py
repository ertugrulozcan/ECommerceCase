from locust import HttpLocust, TaskSet, task


class UserBehavior(TaskSet):
    credentials = {
        "username": "aertugrulozcan@gmail.com",
        "password": ".Abcd1234!"
    }

    headers = {
        'X-Ertis-Alias': '5e652757aac6b140f9250316',
        'Content-Type': 'application/json'
    }

    def on_start(self):
        self.client.verify = False
        self.login()

    def login(self):
        self.client.post(url="/generate-token", json=self.credentials, headers=self.headers)


class WebsiteUser(HttpLocust):
    task_set = UserBehavior
    min_wait = 5000
    max_wait = 9000
