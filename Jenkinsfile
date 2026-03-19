pipeline {
    agent any
    environment{
        IMAGE_NAME="chetan2992/taskmanagerapi"
        IMAGE_TAG="v${env.BUILD_NUMBER}"
    }
    stages {
        stage('Clone') {
            steps {
                git branch: 'main',
                    changelog: false,
                    poll: false,
                    url: 'https://github.com/chetanQss/TaskManagerAPI'
            }
        }
        stage('Build') {
            steps {
                bat 'dotnet restore TaskManagerAPI.sln'
                bat 'dotnet clean TaskManagerAPI.sln'
                bat 'dotnet build TaskManagerAPI.sln --configuration Release'
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test TaskManagerTests/TaskManagerTests.csproj'
            }
        }
        stage('Publish') {
            steps {
                bat 'dotnet publish TaskManagerAPI.csproj -c Release -o publish'
            }
        }
        stage('Build Docker Image') {
            steps {
                bat 'docker build -t %IMAGE_NAME%:%IMAGE_TAG% .'
            }
        }
        stage('Login to DockerHub') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'DockerHub', passwordVariable: 'DockerPass', usernameVariable: 'DockerUser')]) {
                     bat 'docker login -u %DockerUser% -p %DockerPass%'
                }
            }
        }
        stage('Tag Docker Image') {
            steps {
                bat 'docker tag %IMAGE_NAME%:%IMAGE_TAG% %IMAGE_NAME%:latest'
            }
        }
        stage('Push Docker Image') {
            steps {
                bat 'docker push %IMAGE_NAME%:%IMAGE_TAG%'
            }
        }
        stage('Deploy') {
            steps {
                bat '''
                docker ps -a
                docker stop taskmanagerapi-container || echo "No container to stop"
                docker rm taskmanagerapi-container || echo "No container to remove"
                docker run -d -p 5000:80 --name taskmanagerapi-container %IMAGE_NAME%:%IMAGE_TAG%
                docker ps
                '''
            }
        }
        stage('Clean workspace') {
            steps {
                cleanWs()
            }
        }
    }
}