import subprocess
import os
import sys
from git import Repo

nuget = r'c:\ProgramData\chocolatey\lib\NuGet.CommandLine\tools\nuget.exe'
#projects = ["armolib", "auxiliarylib", "datacommunicator", "HtmlDiff", "SandBox", "SatelSdk", "wcfextras", "zkveinsensor", "compassplayer" ]
projects = ["SatelSdk"]


def run_git(args):
    print(args)
    process = subprocess.Popen("git " + args )
    process.wait()
    if process.returncode != 0:
        sys.exit(process.errors)

def nuget_run(self, command):
    print("Nuget " + command)
    if subprocess.call([nuget, command, self.solution_file]) != 0:
        sys.exit(process.errors)

def push_to_nuget(proj):
    temp_dir = "d:\\Temp\\" + proj + "_temp"
    for root, dirs, files in os.walk(temp_dir):
        for file in files:
            if  ".nupkg" in file:
                full_dir = os.path.join(root, file)
                print( "Push " + full_dir)
                subprocess.call([nuget, 'push', full_dir, 'a4ab0201-5c18-41aa-b7a3-24216c5f44e0', '-src', 'http://devserver:81/nuget/ArmoProGet/'])

def pull_all():
    for project in projects:
        project_dir = "d:\\Projects\\" + project
        os.chdir(project_dir)
        repo = Repo(project_dir)
        print('Repository =' + project + ';Active branch = ' + repo.active_branch.name)
        run_git("pull --progress -v --no-rebase \"origin\" " + repo.active_branch.name)

def update_nuget(proj):
    solution = "D:\\Projects\\" + proj + "\\Source\\Main.sln"
    subprocess.call([nuget, 'restore', solution])
    subprocess.call([nuget, 'update', solution])

def update_timex():
    subprocess.call([nuget, 'restore', 'd:\\Projects\\Timex\\Source\\Tests\\Tests.sln'])
    subprocess.call([nuget, 'restore', 'd:\\Projects\\Timex\\Source\\Others\\Others.sln'])
    subprocess.call([nuget, 'restore', 'd:\\Projects\\Timex\\Source\\Main\\Timex.sln'])

    subprocess.call([nuget, 'update', 'd:\\Projects\\Timex\\Source\\Tests\\Tests.sln'])
    subprocess.call([nuget, 'update', 'd:\\Projects\\Timex\\Source\\Others\\Others.sln'])
    subprocess.call([nuget, 'update', 'd:\\Projects\\Timex\\Source\\Main\\Timex.sln'])

def build(proj):
    script = "D:\\Projects\\" + proj + "\\Build\\build.py"
    process = subprocess.Popen("python.exe " + script )
    process.wait()
    if process.returncode != 0:
        sys.exit(process.errors)

def update_all():
    for proj in projects:
        build(proj)
        #update_nuget(proj)
       # build(proj)
        push_to_nuget(proj)

#pull_all()

os.environ['ArmoLibCurrentVersion'] = '1.3.5'
update_all()

#update_timex()