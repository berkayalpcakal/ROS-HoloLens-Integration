#!/usr/bin/env python
# license removed for brevity
import rospy
from std_msgs.msg import String
from moveit_tutorials.msg import TrajectoryPositions
from moveit_msgs.msg import DisplayTrajectory
from sensor_msgs.msg import JointState

class PublishTrajectoryPositions:
    def __init__(self):
        # rospy.init_node('publish_trajectory_positions', anonymous=True)

        self.publisher = rospy.Publisher('trajectory_positions', TrajectoryPositions, queue_size=10)
        self.subcriber = rospy.Subscriber("move_group/display_planned_path/", DisplayTrajectory, self.callback)

        rospy.spin()    

    def callback(self, data):
        trajectoryPositions = TrajectoryPositions()
        
        joint_names = data.trajectory[0].joint_trajectory.joint_names
        points      = data.trajectory[0].joint_trajectory.points

        for i in range(len(points)):

            jointState = JointState()
            for j in range(len(joint_names)):
                jointState.name.append(joint_names[j])
                jointState.position.append(points[i].positions[j])
                jointState.velocity.append(points[i].velocities[j])
                jointState.effort.append(0)

            trajectoryPositions.joint_states.append(jointState)
            trajectoryPositions.time_from_start.append(points[i].time_from_start)
        
        self.publisher.publish(trajectoryPositions)

if __name__ == '__main__':
    try:
        node = PublishTrajectoryPositions()
    except rospy.ROSInterruptException:
        pass