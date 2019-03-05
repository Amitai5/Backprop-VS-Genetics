# Supervised Data in Backpropagation and Genetics Learning Algorithms

The purpose of this research project was to address the efficency of the Backpropagation and Genetics learning algorithms when dealing with supervised data. The efficiency of a learning algorithm was determined by both the speed at which the learning algorithm was able to train a Neural Network along with its overall accuracy at the given task.

## Research Tests

### Test 1: XOR Gate
The goal of this test was to address the efficency of each learning algorithm when it comes to simple tasks such as the XOR Gate. The parameters for each learning algorithm are as follows:

#### Genetics:
* Mutation rate: 0.05
* Population size: 500

#### Backpropagation:
* Learning rate: 0.2
* Momentum rate: 0.0125 

|  | Backpropagation Training Time | Genetics Training Time |
|:-------------:|:-------------:|:-----:|
Minimum: | 163.9362 | 169.9704 |
1st Quartile: | 239.348025 | 214.5134 |
3rd Quartile: | 406.024125 | 335.4027 |
Maximum: | 647.6394 | 1863.1769 |

---

### Test 2: Reptile Classification
The goal of this test was to address the accuracy of each learning algorithm given a limited amount of time. (ADD MORE HERE) The parameters for each learning algorithm are as follows:

#### Genetics:
* Mutation rate: 0.05
* Population size: 125

#### Backpropagation:
* Learning rate: 0.035
* Momentum rate: 0.0125 

![alt text](https://user-images.githubusercontent.com/8137628/53787977-464e3f00-3ed5-11e9-856b-bc8b74094a2c.png "Reptile Classifictation Graph")

## Acknowledgements
A huge thank you to <a href="http://greatmindsrobotics.com/">Great Minds Robotics</a> and <a href="https://github.com/RScopio">Ryan Scopio</a> for their advice/guidance throughout the research project.
