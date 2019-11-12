import subprocess

projects = ["armolib", "auxiliarylib", "datacommunicator", "HtmlDiff", "SandBox", "SatelSdk" ]
projects = ["SandBox"]

def build_all():
    for proj in projects:
        script = "D:\\Projects\\" + proj + "\\Build\\build.py"
        subprocess.call(['python.exe', script])

build_all()