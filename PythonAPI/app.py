from flask import Flask, request, jsonify
import time
import subprocess
import traceback

app = Flask("Learn2Code")

@app.route('/runcode', methods=['POST'])
def run_code():
    data = request.get_json()
    
    code = data.get('code')
    desired_output = data.get('desired_output')
    
    start_time = time.time()
    
    try:
        with open('temp_code.py', 'w') as f:
            f.write(code)
        
        output = subprocess.check_output(['python', 'temp_code.py'], timeout=10, stderr=subprocess.STDOUT)
        output = output.decode('utf-8').strip()
        
        if output == desired_output:
            result = "Success"
        else:
            result = "Failure: Output does not match desired output"
        
    except subprocess.CalledProcessError as e:
        result = "Failure: Error occurred during code execution"
        output = e.output.decode('utf-8').strip()
    except subprocess.TimeoutExpired:
        result = "Failure: Code execution timed out"
        output = ""
    except Exception as e:
        result = "Failure: An unexpected error occurred"
        output = traceback.format_exc()
    finally:
        subprocess.run(['rm', 'temp_code.py'], stdout=subprocess.PIPE, stderr=subprocess.PIPE)

    end_time = time.time()
    execution_time = end_time - start_time

    response = {
        "result": result,
        "output": output,
        "execution_time": execution_time
    }

    return jsonify(response)

if __name__ == '__main__':
    app.run(debug=True)
