Some issues and solutions

When you create a project it will autogen a docker file for you and within VS it will build fine.

However, if you are in the folder *with* the `Dockerfile`, ex: in this case `MsPoc01.csproj` and you try to do a `docker build` from the terminal, it will fail with the error
`failed to compute cache key`. Digging around stackoverflow led me to https://stackoverflow.com/questions/66146088/docker-failed-to-compute-cache-key-not-found-runs-fine-in-visual-studio
which suggests that you need to run the build command from the sln level and point to the docker file you want to use as shown below
`docker build -f src\MsPoc01\Dockerfile --force-rm -t ms1:t1 .`

To push to docker, do a rebuild on the image and tag correctly
`docker build -f src\MsPoc01\Dockerfile --force-rm -t sparkydasrath/test:t1 .`
